using System;

namespace ex25_1_ferramentas.Models
{
    public class ErrorViewModel
    {
        public string Erro { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string erro)
        {
            this.Erro = erro;
        }

    }
}
