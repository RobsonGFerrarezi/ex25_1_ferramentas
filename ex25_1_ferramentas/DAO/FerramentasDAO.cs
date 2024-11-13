using ex25_1_ferramentas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ex25_1_ferramentas.DAO
{
    public class FerramentasDAO
    {
        private SqlParameter[] CriaParametros(FerramentasViewModel ferramenta)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("id", ferramenta.Id);
            parametros[1] = new SqlParameter("descricao", ferramenta.Descricao);
            parametros[2] = new SqlParameter("fabricanteId", ferramenta.FabricanteId);
            return parametros;
        }

        public void Inserir(FerramentasViewModel ferramentas)
        {
            var parametros = CriaParametros(ferramentas);
            Array.Resize(ref parametros, parametros.Length + 1);
            parametros[parametros.Length - 1] = new SqlParameter("operacao", "I");
            HelperDAO.ExecutaProc("spIncluiAlteraFerramenta",parametros);
        }

        public void Alterar(FerramentasViewModel ferramentas)
        {
            var parametros = CriaParametros(ferramentas);
            Array.Resize(ref parametros, parametros.Length + 1);
            parametros[parametros.Length - 1] = new SqlParameter("operacao", "A");
            HelperDAO.ExecutaProc("spIncluiAlteraFerramenta",parametros);
        }

        public void Excluir(int id)
        {
            var parametro = new SqlParameter[]
            {
                new SqlParameter("id",id)
            };
            HelperDAO.ExecutaProc("spExcluiFerramenta", parametro);
        }

        private FerramentasViewModel MontarVM(DataRow registro)
        {
            var ferramenta = new FerramentasViewModel();
            ferramenta.Id = Convert.ToInt32(registro["id"]);
            ferramenta.Descricao = registro["descricao"].ToString();
            ferramenta.FabricanteId = Convert.ToInt32(registro["fabricanteId"]);

            return ferramenta;
        }

        public FerramentasViewModel Consulta(int id)
        {
            var parametro = new SqlParameter[]
            {
                new SqlParameter("id",id)
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaFerramenta", parametro);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontarVM(tabela.Rows[0]);
        }

        public List<FerramentasViewModel> Listagem()
        {
            var lista = new List<FerramentasViewModel>();

            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemFerramenta", null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontarVM(registro));

            return lista;
        }
    }

    /*
    create procedure spIncluiAlteraFerramenta(
    @operacao varchar(1),
    @id int,
    @descricao varchar(50),
    @fabricanteId int
    )
    as
    begin
      if (@operacao = 'I')
      begin
          insert into ferramentas (descricao, fabricanteId)
	      values
	      (@descricao, @fabricanteId)
      end
      else
      begin
         update ferramentas set descricao = @descricao, fabricanteId = @fabricanteId
	     where id = @id
      end 
    end
    GO

    create procedure spExcluiFerramenta
    (
	    @id int
    )
    as
    begin
	    delete ferramentas where id = @id
    end
    GO

    create procedure spConsultaFerramenta
    (
	    @id int
    )
    as
    begin
	    select * from ferramentas where id = @id
    end
    GO

    create procedure spListagemFerramenta
    as
    begin
	    select * from ferramentas order by descricao
    end
    GO
     */
}
