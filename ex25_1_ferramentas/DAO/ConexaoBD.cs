using System.Data.SqlClient;

namespace ex25_1_ferramentas.DAO
{
    internal static class ConexaoBD
    {
        internal static SqlConnection GetConexao()
        {
            string strConexao = "Data Source=LOCALHOST;Initial Catalog=AULADB;user id=sa; password=123456";
            SqlConnection conexao = new SqlConnection(strConexao);
            conexao.Open();
            return conexao;
        }
    }
}
