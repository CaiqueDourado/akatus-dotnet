using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _ConsultaParcelamento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Akatus.ConsultaParcelamento.Consulta consulta = new Akatus.ConsultaParcelamento.Consulta();
            
            //Consulta opções de parcelamento
            Akatus.ConsultaParcelamento.Retorno retorno = consulta.consultaParcelamento(10.39m, Akatus.Enums.MeioDePagamento.cartao_visa);

            //Se os dados foram postados com sucesso
            if (retorno != null)
            {
                Response.Write("Descricao = " + retorno.Descricao + "<br />");
                Response.Write("ParcelasAssumidas = " + retorno.ParcelasAssumidas + "<br />");

                foreach (Akatus.ConsultaParcelamento.RetornoParcela parcela in retorno.Parcelas)
                {
                    Response.Write("Quantidade = " + parcela.Quantidade + "<br />");
                    Response.Write("Total = " + parcela.Total + "<br />");
                    Response.Write("Valor = " + parcela.Valor + "<br /><br />");
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