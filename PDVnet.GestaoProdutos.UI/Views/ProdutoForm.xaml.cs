using System.Globalization;
using System.Windows;
using PDVnet.GestaoProdutos.Model;
using PDVnet.GestaoProdutos.UI.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDVnet.GestaoProdutos.UI.Views
{
    public partial class ProdutoForm : Window
    {
        private bool _isEditingPreco = false;
        private static readonly CultureInfo _br = new CultureInfo("pt-BR");

        public ProdutoForm()
        {
            InitializeComponent();
            DataContext = new ProdutoFormViewModel();
        }

        public ProdutoForm(Produto produto)
        {
            InitializeComponent();
            DataContext = new ProdutoFormViewModel(produto);

            Loaded += (s, e) =>
            {
                txtPreco.Text = produto.Preco.ToString("N2", _br);
            };
        }

        private void TxtPreco_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }

        private void TxtPreco_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TxtPreco_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isEditingPreco)
            {
                return;
            }

            _isEditingPreco = true;

            if (!(sender is TextBox tb)) { _isEditingPreco = false; return; }

            string digits = Regex.Replace(tb.Text, @"\D", "");

            if (string.IsNullOrEmpty(digits))
            {
                tb.Text = "0,00";
                tb.CaretIndex = tb.Text.Length;
                UpdateViewModelPreco(0m);
                _isEditingPreco = false;
                return;
            }

            if (decimal.TryParse(digits, out decimal cents))
            {
                decimal value = cents / 100m;
                tb.Text = value.ToString("N2", _br);
                tb.CaretIndex = tb.Text.Length;
                UpdateViewModelPreco(value);
            }

            _isEditingPreco = false;
        }

        private void UpdateViewModelPreco(decimal value)
        {
            if (DataContext is ProdutoFormViewModel vm && vm.Produto != null)
            {
                vm.Produto.Preco = value;
            }
        }

        private void TxtQuantidade_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d$");
        }

        private void TxtQuantidade_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Subtract)
            {
                e.Handled = true;
            }
        }

        private void TxtQuantidade_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && tb.Text == "0")
            {
                tb.Clear();
            }
        }

        private void TxtQuantidade_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox tb))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(tb.Text) || !int.TryParse(tb.Text, out int value))
            {
                tb.Text = "0";
                value = 0;
            }

            if (DataContext is ProdutoFormViewModel vm && vm.Produto != null)
            {
                vm.Produto.Quantidade = value;
            }
        }
    }
}
