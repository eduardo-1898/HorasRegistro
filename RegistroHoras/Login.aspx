<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Formulario_4.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <title>Inicio de sesión</title>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>

<body style="background-image: url(imagenes/AST.jpg); background-size: cover;">

    <div class="login">
        <form runat="server">

            <div class="container col-md-4" style="margin-top:15%;">
                <div class="card bg-dark">
                    <div class="card-body">
                        <h3 class="text-white text-center">Bienvenido</h3>

                        <div class="form-group">
                            <label for="user">Usuario </label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUsuario" placeholder="Nombre de Usuario" />
                        </div>

                        <div class="form-group mb-3">
                            <label for="contraseña">Contraseña </label>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtContrasena" CssClass="form-control" placeholder="Contraseña" />
                        </div>
                        <asp:Button runat="server" CssClass="btn w-100 text-white" ID="btnIngresar" BackColor="#FF6031" Text="Ingresar" Style="margin-top: 10px" OnClick="btnIngresar_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </form>
    </div>

</body>
</html>
