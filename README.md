Akatus - Integração com .NET
=================
Biblioteca para integração da API Akatus com .NET
por Caique Dourado - http://www.caiquedourado.com.br


Configuração
=================
Adicione no Web.Config sua API Key, o Tóken NIP, o E-mail utilizado no cadastro da Akatus, e o Ambiente ('producao'
ou 'testes'), ficando dessa forma:
```c#
<appSettings>

  <!-- Akatus - Ambiente ('producao' ou 'testes')-->
  <add key="AkatusAmbiente" value="testes"/>
  
  <!-- Akatus - API Key -->
  <add key="AkatusApiKey" value="SUA-API-KEY"/>
  
  <!-- Akatus - Tóken NIP -->
  <add key="AkatusTokenNIP" value="SEU-TOKEN-NIP"/>
  
  <!-- Akatus - E-mail da Conta -->
  <add key="AkatusEmail" value="emaildecadastro@empresa.com.br"/>
  
</appSettings>
```

CARRINHO
=================

PROCESSAR TRANSAÇÃO
-----------------
```c#

```

catch (Exception ex) {
  //Show excepction message
Response.Write(ex.Message); 
}
```
