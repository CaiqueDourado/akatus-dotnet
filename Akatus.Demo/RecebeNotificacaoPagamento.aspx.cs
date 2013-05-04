using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecebeNotificacaoPagamento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Pega parâmetros postados
        string token = Request.Form["token"];
        string transacao_id = Request.Form["transacao_id"];
        string status = Request.Form["status"];
        string referencia = Request.Form["referencia"];

        //Processa retono da transação
        Akatus.NotificacaoPagamento.Retorno retorno = Akatus.NotificacaoPagamento.Notificacao.processaRetorno(token, transacao_id, status, referencia);

        if (retorno != null)
        {
            Response.Write("Status = " + retorno.Status.ToString() + "<br />");
            Response.Write("TransacaoId = " + retorno.TransacaoId + "<br />");
            Response.Write("Referencia = " + retorno.Referencia + "<br />");
        }
        else
        {
            Response.Write("Não foram retornados dados");
        }

    }

    
}