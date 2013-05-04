using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _ConsultaMeiosPagamento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Akatus.ConsultaMeiosPagamento.Consulta consulta = new Akatus.ConsultaMeiosPagamento.Consulta();

            //Consulta meios de pagamento
            List<Akatus.ConsultaMeiosPagamento.Retorno> retorno = consulta.consultaMeiosDePagamentoDisponiveis();

            //Se os dados foram postados com sucesso
            if (retorno != null)
            {
                foreach (Akatus.ConsultaMeiosPagamento.Retorno parcela in retorno)
                {
                    Response.Write("Codigo = " + parcela.Codigo + "<br />");
                    Response.Write("Descricao = " + parcela.Descricao + "<br />");
                    Response.Write("Parcelas = " + parcela.Parcelas + "<br /><br />");
                }
            }
            else
            {
                Response.Write("Não foram retornados dados");
            }
        }
        catch (Akatus.RestExcepction ex)
        {
            //Show http status
            Response.Write(string.Format("statusCode =  {0} <br /> statusDescription = {1}", ex.StatusCode, ex.StatusDesciption));

            //Show error messages
            foreach (string error in ex.ErrorMessages)
                Response.Write("<br />" + error);
        }
        catch (Exception ex) {

            Response.Write(ex);

        }


    }

    
}