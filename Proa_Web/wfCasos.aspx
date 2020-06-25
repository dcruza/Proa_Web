<%@ Page Title="Casos Registrados" Language="C#" MasterPageFile="~/wfMasterProa.Master" AutoEventWireup="true" CodeBehind="wfCasos.aspx.cs" Inherits="Proa_Web.wfCasos" %>
<%@ Register src="UserControl/mensajes.ascx" tagname="mensajes" tagprefix="uc1" %>
<asp:Content ID="tit" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lblTitulo" CssClass="profile-text" runat="server" Text="Gestión de Casos Abiertos" Font-Size="Larger" Font-Italic="true" ForeColor="White"></asp:Label>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="smgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upl" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>

            <%---------------------------------------------------------%>
            <%------------------ INICIA FORMULARIO --------------------%>
            <%---------------------------------------------------------%>

            <!-- ============================================================== -->
            <!-- Inicia texto de navegación -->
            <!-- ============================================================== -->
            <div class="row page-titles">
                <div class="col-md-6 col-8 align-self-center">
                    <h3 class="text-themecolor m-b-0 m-t-0">Casos Abiertos</h3>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                        <li class="breadcrumb-item active">Casos</li>
                    </ol>
                </div>
                <div class="col-md-6 col-8 align-self-center">
                    <div id="console-event"></div>
                </div>
            </div>
            <!-- ============================================================== -->
            <!-- Finaliza texto de navegación -->
            <!-- ============================================================== -->
            
            <!-- ============================================================== -->
            <!-- Inicia Selección de Especialidad y otros -->
            <!-- ============================================================== -->
            <div class="row" >                
                <div class="col-lg-12 col-xlg-12 col-md-12">                  
                    <div class="card border-primary">
                        <div class="card-block">
                            <class="m-t-30">                                
                                <h6 class="card-subtitle">Establezca los filtros deseados</h6>
                                <div class="row text-left justify-content-md-start ">
                                    <div class="col-2 col-form-label">
                                        <asp:label id="lblNomServicio" runat="server" text="Servicio"></asp:label>
                                    </div>
                                    <div class="col-3">
                                        <asp:DropDownList ID="ddlServicio" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>                                     
                                    <div class="col-3 checkbox checkbox-circle checkbox-success">
                                        <asp:CheckBox runat="server" ID="rdoNuewvosPacs" Text="Seguimiento" AutoPostBack="true" />
                                    </div>
                                    <div class="col-4 ">
                                        <asp:Button ID="btnAgregarNuevoCaso" runat="server" Text="Agregar un nuevo caso" PostBackUrl="~/wfPaciente.aspx" CssClass="btn btn-outline-info"></asp:Button>
                                    </div>
                                </div>
                                <div class="row text-left justify-content-md-start ">
                                    <div class="col-2 col-form-label">
                                        <asp:label id="lblNomEspecialidad" runat="server" text="Especialidad"></asp:label>
                                    </div>
                                    <div class="col-3">
                                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged"></asp:DropDownList>
                                    </div>                                    
                                </div>
                            </class>
                        </div>
                    </div>
                </div>
            </div>
          <!-- ============================================================== -->
          <!-- Finaliza Selección de Especialidad y otros -->
          <!-- ============================================================== -->
          
          <!-- ============================================================== -->
          <!-- Inicia grid de casos abiertos -->
          <!-- ============================================================== -->
          <div class="row" >                
            <div class="col-lg-12 col-xlg-12 col-md-12">                  
                <div class="card border-primary">
                    <div class="card-block">
                        <class="">                                
                            <h6 class="card-subtitle">Casos Registrados</h6>
                            <div class="row text-left justify-content-md-start ">
                                <div class="col-12 col-form-label">
                                    <asp:GridView ID="grdResultadoBusq" runat="server" CssClass="table table-condensed table-hover table-responsive table-sm"
                                            AutoGenerateColumns="False" EmptyDataText="No se econtraron registros" AllowPaging="True"
                                            PageSize="10" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnSeleccionar" runat="server" ImageUrl="~/image/icons/icon_edit_p.png" CommandName="EDITAR"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idPaciente" HeaderText="ID" />                                                             
                                                <asp:BoundField DataField="nombrePac" HeaderText="Nombre" />
                                                <asp:BoundField DataField="ServicioInicial" HeaderText="Servicio"/>
                                                <asp:BoundField DataField="EspecialidadInicial" HeaderText="Especialidad"/>                                                                                                
                                                <asp:BoundField DataField="fechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}"/>  
                                                <asp:BoundField DataField="fechaUltSeguimiento" HeaderText="Último Seguim"/>                                                                                                
                                                <asp:BoundField DataField="Estado" HeaderText="Estado"/>                                                                                                
                                            </Columns>
                                            <RowStyle ForeColor="#000066" Font-Size="10" />
                                        <SelectedRowStyle BackColor="#55acee" Font-Bold="True" ForeColor="White" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="cssPager" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        </asp:GridView>
                                </div>                                
                            </div>
                        </class>
                    </div>
                </div>
                
            </div>
          </div>
          <!-- ============================================================== -->
          <!-- Finaliza grid de casos abiertos -->
          <!-- ============================================================== -->

            <%---------------------------------------------------------%>
            <%------------------ FINALIZA FORMULARIO --------------------%>
            <%---------------------------------------------------------%>
            <uc1:mensajes ID="msgBox" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
