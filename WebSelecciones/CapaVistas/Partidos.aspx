<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Partidos.aspx.cs" Inherits="WebSelecciones.CapaVistas.Partidos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../css/MenuCss.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <nav class="menu">
                <input id="menu__toggle" type="checkbox" class='menu__toggle' />
                <label for="menu__toggle" class="menu__toggle-label">
                    <svg preserveAspectRatio='xMinYMin' viewBox='0 0 24 24'>
                        <path d='M3,6H21V8H3V6M3,11H21V13H3V11M3,16H21V18H3V16Z' />
                    </svg>
                    <svg preserveAspectRatio='xMinYMin' viewBox='0 0 24 24'>
                        <path d="M19,6.41L17.59,5L12,10.59L6.41,5L5,6.41L10.59,12L5,17.59L6.41,19L12,13.41L17.59,19L19,17.59L13.41,12L19,6.41Z" />
                    </svg>
                </label>
                <ol class='menu__content'>
                    <li class="menu-item"><a href="Menu.aspx" >Menu Principal</a></li>
                    <li class="menu-item"><a href="Partidos.aspx">Partidos Disponibles </a></li>
                    <li class="menu-item"><a href="RegistroCandidatos.aspx" >Registrar Candidatos</a></li>
                    <li class="menu-item"><a href="RegistroVotos.aspx">Votar </a></li>
                    <li class="menu-item"><a href="Resultados.aspx">Resultados </a></li>
                    
                </ol>
            </nav>
            
            <!-- Mensaje de bienvenida -->
            <div class="mensaje-bienvenida">
                Partidos Politicos Disponibles
                <br />
                <br />

               


                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>

                <br />
                <br />
 
            <!-- Aquí se cargarán dinámicamente los candidatos registrados -->

     
                
            </div>
        </div>
    </form>
</body>
</html>
