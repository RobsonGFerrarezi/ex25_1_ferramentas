using Microsoft.AspNetCore.Mvc.Formatters;
using System.Data;
using System.Data.SqlClient;

namespace ex25_1_ferramentas.DAO
{
    internal static class HelperDAO
    {
        internal static void ExecutaSQL(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand (sql, conexao))
                {
                    if(parametros != null)
                        comando.Parameters.AddRange (parametros);
                    comando.ExecuteNonQuery ();
                }
                conexao.Close ();
            }
        }

        internal static void ExecutaSQL(string sql, SqlParameter parametro)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(sql, conexao))
                {
                    if (parametro != null)
                        comando.Parameters.Add(parametro);
                    comando.ExecuteNonQuery();
                }
                conexao.Close();
            }
        }

        internal static DataTable ExecuteSelect(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    conexao.Close();
                    return tabela;
                }
            }
        }

        internal static void ExecutaProc(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(nomeProc,conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();
                }
            }
        }

        internal static DataTable ExecutaProcSelect(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using(SqlDataAdapter adapter = new SqlDataAdapter(nomeProc, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange (parametros);

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    return tabela;
                }
            }
        }


    }
}
