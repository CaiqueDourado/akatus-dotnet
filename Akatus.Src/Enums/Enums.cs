using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Akatus.Enums
{
    /// <summary>
    /// Enumerador para o tipo de Ambiente
    /// </summary>
    public enum Ambiente
    {
        testes,
        producao
    }

    /// <summary>
    /// Enumerador para os tipos de Pagamento
    /// </summary>
    public enum MeioDePagamento
    {
        boleto,
        tef_itau,
        tef_bradesco,
        cartao_visa,
        cartao_master,
        cartao_amex,
        cartao_elo,
        cartao_diners
    }

    /// <summary>
    /// Enumerador para os tipos de Telefone
    /// </summary>
    public enum TipoTelefone
    {
        comercial,
        residencial,
        celular
    }

    /// <summary>
    /// Enumerador para os tipos de Endereço
    /// </summary>
    public enum TipoEndereco
    {
        entrega,
        comercial
    }

    /// <summary>
    /// Enumerador para o status da transação
    /// </summary>
    public enum StatusTransacao
    {
        aguardandoPagamento,
        emAnalise,
        aprovado,
        cancelado,
        processando,
        completo,
        devolvido,
        estornado,
        chargeback
    }
}
