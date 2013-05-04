using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _ConsultaStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
        try
        {
            Akatus.ConsultaStatus.Consulta consulta = new Akatus.ConsultaStatus.Consulta();

            //Consulta status da transação
            Akatus.ConsultaStatus.Retorno retorno = consulta.consultaStatusTransacao("42243005-4382-4781-896b-0437257ea649");

            //Se os dados foram postados com sucesso
            if (retorno != null)
            {
                Response.Write("status = " + retorno.Status + "<br />");
                Response.Write("DataCriacao = " + retorno.DataCriacao + "<br />");
                Response.Write("DataStatusAtual = " + retorno.DataStatusAtual + "<br />");
                Response.Write("referencia = " + retorno.Referencia + "<br />");
                Response.Write("Status = " + retorno.Status.ToString() + "<br />");
                Response.Write("Valor = " + string.Format("{0:c}", retorno.Valor) + "<br />");
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