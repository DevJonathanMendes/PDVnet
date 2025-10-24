using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PDVnet.GestaoProdutos.Model;
using PDVnet.GestaoProdutos.Business;

namespace PDVnet.GestaoProdutos.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ProdutoService _service = new ProdutoService();
        public ObservableCollection<Produto> Produtos { get; set; }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set { _produtoSelecionado = value; OnPropertyChanged(); }
        }

        public int TotalProdutos => Produtos?.Count ?? 0;
        public decimal ValorTotalEstoque => Produtos?.Sum(p => p.Preco * p.Quantidade) ?? 0;
        public int ProdutosBaixoEstoque => Produtos?.Count(p => p.Quantidade < 5) ?? 0;

        public ICommand NovoCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand AtualizarCommand { get; }

        public MainViewModel()
        {
            Produtos = new ObservableCollection<Produto>(_service.Listar());
            NovoCommand = new RelayCommand(NovoProduto);
            EditarCommand = new RelayCommand(EditarProduto, _ => ProdutoSelecionado != null);
            ExcluirCommand = new RelayCommand(ExcluirProduto, _ => ProdutoSelecionado != null);
            AtualizarCommand = new RelayCommand(_ => AtualizarLista());
        }

        private void NovoProduto(object obj)
        {
            var form = new Views.ProdutoForm();
            form.ShowDialog();
            AtualizarLista();
        }

        private void EditarProduto(object obj)
        {
            var form = new Views.ProdutoForm(ProdutoSelecionado);
            form.ShowDialog();
            AtualizarLista();
        }

        private void ExcluirProduto(object obj)
        {
            if (ProdutoSelecionado == null) return;

            if (MessageBox.Show("Deseja realmente excluir este produto?", "Confirmação",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _service.Excluir(ProdutoSelecionado.Id);
                AtualizarLista();
            }
        }

        private void AtualizarLista()
        {
            Produtos.Clear();
            foreach (var p in _service.Listar())
                Produtos.Add(p);
            OnPropertyChanged(nameof(TotalProdutos));
            OnPropertyChanged(nameof(ValorTotalEstoque));
            OnPropertyChanged(nameof(ProdutosBaixoEstoque));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
