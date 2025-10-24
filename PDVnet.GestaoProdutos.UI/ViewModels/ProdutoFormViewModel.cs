using System;
using System.Windows;
using System.Windows.Input;
using PDVnet.GestaoProdutos.Business;
using PDVnet.GestaoProdutos.Model;

namespace PDVnet.GestaoProdutos.UI.ViewModels
{
    public class ProdutoFormViewModel
    {
        private readonly ProdutoService _service = new ProdutoService();
        public Produto Produto { get; set; }
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        public ProdutoFormViewModel(Produto produto = null)
        {
            Produto = produto ?? new Produto();
            SalvarCommand = new RelayCommand(Salvar);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private void Salvar(object obj)
        {
            try
            {
                if (Produto.Id == 0)
                {
                    _service.Adicionar(Produto);
                }
                else
                {
                    _service.Atualizar(Produto);
                }

                MessageBox.Show("Produto salvo com sucesso!");
                ((Window)obj)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancelar(object obj)
        {
            ((Window)obj)?.Close();
        }
    }
}
