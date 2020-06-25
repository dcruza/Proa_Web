﻿<%@ Page Title="Estancias Registradas" Language="C#" AutoEventWireup="true" MasterPageFile="~/wfMasterProa.Master"  CodeBehind="wfAbrirEstancia.aspx.cs" Inherits="Proa_Web.wfAbrirEstancia" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/UserControl/casoPaciente.ascx" TagPrefix="uc1" TagName="casoPaciente" %>
<%@ Register src="UserControl/mensajes.ascx" tagname="mensajes" tagprefix="uc2" %>

<asp:Content ID="tit" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lblTitulo" CssClass="profile-text" runat="server" Text="Gestión de Casos Abiertos" Font-Size="Larger" Font-Italic="true" ForeColor="White"></asp:Label>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="smgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardar" />
            <asp:PostBackTrigger ControlID="btnCerrar" />
        </Triggers>
        <ContentTemplate>
            <!-- ============================================================== -->
            <!-- Inicia texto de navegación -->
            <!-- ============================================================== -->
            <div class="row page-titles">
                <div class="col-md-6 col-8 align-self-center">
                    <h3 class="text-themecolor m-b-0 m-t-0">Pacientes</h3>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                        <li class="breadcrumb-item"><a href="wfPaciente.aspx">Pacientes</a></li>
                        <li class="breadcrumb-item active">Estancias</li>
                    </ol>
                </div>
                <div class="col-md-6 col-8 align-self-center">            
                    <div id="console-event"></div>
                </div>
            </div>
            <!-- ============================================================== -->
            <!-- Finaliza texto de navegación -->
            <!-- ============================================================== -->
            <%--<uc1:casoPaciente runat="server" id="casoPaciente"  />--%> 
            <div class="container" style="">
                <div class="row">
                    <div class="col-12">
                        <!-- ============================================================== -->
                        <div class="row">
                            <!-- ============================================================== -->
                            <!-- Inicia Casos o Estancias abiertas -->
                            <!-- ============================================================== -->
                            <div class="col-lg-12 col-xlg-12 col-md-12">
                                <div class="card border-primary">
                                    <div class="card-block">
                                        <%-- <class="m-t-30">      --%>
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-12 col-form-label" style="background-color: lightcyan">
                                                <h4>Estancias Registradas</h4>
                                            </div>
                                        </div>
                                        <div class="row text-left justify-content-md-start radio-inline">
                                            <div class="col-12">
                                                <asp:GridView ID="grdResultadoBusq" runat="server" CssClass="table table-condensed table-hover table-responsive table-sm"
                                                    AutoGenerateColumns="False" EmptyDataText="No se encontraron registros" AllowPaging="True"
                                                    PageSize="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt" OnRowDataBound="grdResultadoBusq_RowDataBound" OnRowCommand="grdResultadoBusq_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibtnAgregar" runat="server" ImageUrl="~/image/icons/icon_add.png" CommandName="AGREGAR" Height="17px" ToolTip="Importar Caso" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="numCaso" HeaderText="Caso" />
                                                        <asp:TemplateField HeaderText="Origen">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrigen" runat="server" Text="Origen" Font-Bold="true" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="idPaciente" HeaderText="ID" />
                                                        <asp:BoundField DataField="NombrePac" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="ServicioActual" HeaderText="Servicio" />
                                                        <asp:BoundField DataField="EspecialidadInicial" HeaderText="Especialidad" />
                                                        <asp:BoundField DataField="fechaIngreso" HeaderText="Ingreso" DataFormatString="{0:dd/MM/yyyy}" />                                            
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdfCodServ" runat="server" Value='<%# Eval("codServicioInicial") %>' />
                                                                <asp:HiddenField ID="hdfCodEspec" runat="server" value='<%# Eval("codEspecialidadInicial") %>'/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle ForeColor="#000066" Font-Size="Small" />
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
                                        <%-- </class>--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- ============================================================== -->
                        <!-- Inicia Sección Indicar Estancia manualmente-->
                        <!-- ============================================================== -->
                        <div class="row">
                            <div class="col-12">
                                <div class="card border-secondary">
                                    <div class="card-block">
                                        <%--<div class="m-t-30">               --%>
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-12 col-form-label" style="background-color: whitesmoke">
                                                <h4>Registro de Estancia</h4>
                                            </div>
                                        </div>
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-3 col-form-label">
                                                <asp:Label ID="lblFechaIngreso" runat="server" Text="Fecha Ingreso *"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox ID="txtFechaIngreso" TextMode="Date" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                            </div>
                                            <div class="col-2 col-form-label">
                                                <asp:Label ID="lblCaso" runat="server" Text="N° Caso *"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox ID="txtCaso" CssClass="form-control form-control-sm" TextMode="Number" runat="server" Text=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-3 col-form-label">
                                                <asp:Label ID="lblServicio" runat="server" Text="Servicio *"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:DropDownList ID="ddlServicio" CssClass="form-control form-control-sm"
                                                    runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged" Font-Size="X-Small">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-2 col-form-label">
                                                <asp:Label ID="lblEspec" runat="server" Text="Especialidad*"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:DropDownList ID="ddlEspec" CssClass="form-control form-control-sm" runat="server" Font-Size="X-Small"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--</div>--%>
                                    </div>
                                </div>                    
                            </div>
                        </div>


                        <!---------------------------------------------------------------->
                        <!------------- DXs ---------------------------------------------->
                        <!---------------------------------------------------------------->

                        <!-- ============================================================== -->
                        <!-- Inicia Sección Indicar Estancia manualmente-->
                        <!-- ============================================================== -->


                        <div class="row">
                            <div class="col-5">
                                <div class="card border-secondary">
                                    <div class="card-block">
                                        <%--                       <class="m-t-30">     --%>
                                        <!---------------------------------------------------------------->
                                        <!--------INICIO DXs --------------------------------------------->
                                        <!---------------------------------------------------------------->
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-12 col-form-label" style="background-color: whitesmoke">
                                                <h6>Buscar Dx *</h6>
                                            </div>
                                        </div>
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-6 col-form-label">
                                                <asp:Label ID="lblCodDx" runat="server" Text="Código"></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="txtCodDx" runat="server" CssClass="form-control form-control-sm" Font-Size="Small"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-6 col-form-label">
                                                <asp:Label ID="lblDescDx" runat="server" Text="Descrip"></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="txtDescDx" runat="server" CssClass="form-control form-control-sm" Font-Size="Small"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row text-left justify-content-md-start">
                                            <div class="col-6 col-form-label">
                                            </div>
                                            <div class="col-6 col-form-label text-right">
                                                <asp:Button ID="btnBuscaDx" runat="server" Text="Buscar Dx" class="btn btn-sm btn-outline-info col12" OnClick="btnBuscar_Click"></asp:Button>
                                            </div>
                                        </div>
                                        <!---------------------------------------------------------------->
                                        <!------------- FIN DXs ------------------------------------------>
                                        <!---------------------------------------------------------------->
                                        <%--                      </class>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-7">
                                <div class="card border-secondary">
                                    <div class="card-block">
                                       <asp:GridView ID="grdDx" Width="100%" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="9"
                                           AllowPaging="true" PageSize="3" OnPageIndexChanging="grdDx_PageIndexChanging" OnRowCommand="grdDx_RowCommand">
                                           <Columns>
                                               <asp:BoundField DataField="Cod4d" HeaderText="Cód" />
                                               <asp:BoundField DataField="Descripcion" HeaderText="Desc" />
                                               <asp:TemplateField>
                                                   <ItemTemplate>
                                                       <asp:ImageButton ID="ibtnNuevoCaso" runat="server" ImageUrl="~/image/icons/icon_edit_p.png" CommandName="SELECT" ToolTip="Seleccionar" />
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                           </Columns>
                                           <FooterStyle BackColor="White" ForeColor="#000066" />
                                           <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                           <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="cssPager"/>
                                           <RowStyle ForeColor="#000066" />
                                           <SelectedRowStyle BackColor="#55acee" Font-Bold="True" ForeColor="White" />
                                           <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                           <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                           <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                           <SortedDescendingHeaderStyle BackColor="#00547E" />
                                       </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div Class="col-8 text-right">
                                <%--<asp:Panel ID="pnlErrors" runat="server" CssClass="alert alert-danger" style="font-size:small; height:32px; vertical-align:top" role="alert">
                                   <asp:Label ID="lblErrorMsg" runat="server" Text="Indique los Campos requeridos (*)"></asp:Label>
                                </asp:Panel>--%>
                            </div>
                            <div class="col-4 text-right">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-twitter" OnClick="btnGuardar_Click"></asp:Button>
                                <asp:Button ID="btnCerrar" runat="server" Text="Cancelar" CssClass="btn btn-warning" OnClick="btnCerrar_Click" ></asp:Button>                    
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <uc2:mensajes ID="msgBox" runat="server" />
            <asp:HiddenField ID="hdfIpPac" runat="server" />
            <asp:HiddenField ID="hdfCasoArca" runat="server" />
            <asp:HiddenField ID="hdfIdServ" runat="server" />
            <asp:HiddenField ID="hdfIdEspec" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


