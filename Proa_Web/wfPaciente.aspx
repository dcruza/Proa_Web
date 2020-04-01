<%@ Page Title="Gestión de Pacientes" Language="C#" MasterPageFile="~/wfMasterProa.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="wfPaciente.aspx.cs" Inherits="Proa_Web.wfPaciente" %>

<%@ Register src="UserControl/mensajes.ascx" tagname="mensajes" tagprefix="uc1" %>

<asp:Content ID="tit" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lblTitulo" CssClass="profile-text" runat="server" Text="Gestión de Pacientes" Font-Size="Larger" Font-Italic="true" ForeColor="White"></asp:Label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="smgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBuscar" />
            <asp:PostBackTrigger ControlID="btnGuardarPac" />
            <asp:PostBackTrigger ControlID="btnCancelar" />
            <asp:PostBackTrigger ControlID="grdResultadoBusq" />
            <asp:PostBackTrigger ControlID="msgBox" />            
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
                <li class="breadcrumb-item active">Pacientes</li>
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
    <div class="row" >
        <!-- ============================================================== -->
        <!-- Inicia Columna de Tipo de Inserción de Pacientes -->
        <!-- ============================================================== -->
        <div class="col-lg-12 col-xlg-12 col-md-12">                  
            <div class="card border-primary">
                <div class="card-block">
                    <class="m-t-30">
<%--                        <h4 class="card-title m-t-10">Indique el tipo de Paciente</h4>--%>
                        <div class="row text-left justify-content-md-start radio-inline">
                            <div class="col-5 col-lg-5 col-md-5 col-sm-5">
                                <asp:RadioButton runat="server" ID="rdoBuscarPacs" Text="Búsqueda de Pacientes" CssClass="radio radio-success RADI" Checked="true"
                                    GroupName="rdoTipoBusq" AutoPostBack="true" OnCheckedChanged="rdoBuscarPacs_CheckedChanged" />                                
                            </div>
                            <div class="col-5 col-lg-5 col-md-5 col-sm-5 radio-inline">
                                <asp:RadioButton runat="server" ID="rdoNuewvosPacs" Text="Crear Paciente Nuevo" CssClass="radio radio-warning"
                                    GroupName="rdoTipoBusq" AutoPostBack="true" OnCheckedChanged="rdoBuscarPacs_CheckedChanged"/>
                            </div>
                        </div>
                    </class>
                </div>
            </div>
        </div>
    </div>
  <!-- ============================================================== -->
    <div class="row">        
    </div>       
    <!-- ============================================================== -->
            <!-- Inicia Page Content -->
            <!-- ============================================================== -->
            <!-- Row -->
            <div class="row " id="pnlBusq" >
                <!-- ============================================================== -->
                <!-- Columna de Búsqueda de Pacientes -->
                <!-- ============================================================== -->
                <asp:Panel id="pnlBusq1" CssClass="col-lg-4 col-xlg-3 col-md-5" runat="server">
                    <div class="card" style="height:312px">
                        <div class="card-block">
                            <class="m-t-30"> 
                                <h4 class="card-title m-t-10">Buscar Paciente</h4>
                                <h6 class="card-subtitle">Establezca los filtros deseados</h6>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-5 col-form-label"><asp:label id="lblIdPacBusq" runat="server" text="Identificación"></asp:label> </div>
                                    <div class="col-7"><asp:TextBox ID="txtIdPacBusq" CssClass=" form-control form-control-sm" runat="server" Text=""></asp:TextBox></div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-5 col-form-label"><asp:label id="lblNomPacBusq" runat="server" text="Nombre"></asp:label></div>
                                    <div class="col-7"><asp:TextBox ID="txtNomPacBusq" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox></div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-5 col-form-label"><asp:label id="lblApel1PacBusq" runat="server" text="Apellido 1"></asp:label></div>
                                    <div class="col-7"><asp:TextBox ID="txtApel1PacBusq" CssClass=" form-control form-control-sm" runat="server" Text=""></asp:TextBox></div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-5 col-form-label"><asp:label id="lblApel2PacBusq" runat="server" text="Apellido 2"></asp:label></div>
                                    <div class="col-7"><asp:TextBox ID="txtApel2PacBusq" CssClass=" form-control form-control-sm" runat="server" Text=""></asp:TextBox></div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <%--<div class="col-5 col-form-label">                                        
                                    </div>--%>
                                    <div class="col-10 col-form-label">
                                        <asp:CheckBox ID="chkSoloBuscArcaMedisys" CssClass="col-form-label-sm" runat="server" Text="- Búsqueda Externa" />
                                    </div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-0 col-form-label">                                        
                                    </div>
                                    <div class="col-12">     
                                       <div class="row">
                                           <div class="col-6">
                                                <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-twitter btn-sm btn-block" Text="Buscar" OnClick="btnBuscar_Click"></asp:Button>
                                           </div>
                                           <div class="col-6">
                                                <asp:Button ID="btnNuevaBusq" runat="server" CssClass="btn btn-facebook btn-sm btn-block" Text="Limpiar" OnClick="btnNuevaBusq_Click" ></asp:Button>
                                           </div>
                                        </div>
                                    </div>
                                </div>                                                      
                            </class>
                        </div>
                    </div>
                </asp:Panel>
                <!-- ============================================================== -->
                <!-- Columna de Resultados de Búsqueda de Pacientes -->
                <!-- ============================================================== -->
                <asp:Panel ID="pnlBusq2" runat="server" CssClass="col-lg-8 col-xlg-9 col-md-7">
                    <div class="card" style="height:312px">
                        <div class="card-block">
                            <class="m-t-30 ">
                                 <h4 class="card-title m-t-10">Resultados de la Búsqueda</h4>                                                           
                                 <h6 class="card-subtitle">Seleccione el paciente para ver detalle</h6>
                                 <div class="row text-left justify-content-md-start">
                                    <div class="col-12 col-form-label">
                                        <asp:GridView ID="grdResultadoBusq" runat="server" CssClass="table table-condensed table-hover table-responsive table-sm" 
                                            AutoGenerateColumns="False" EmptyDataText="No se econtraron registros" OnRowCommand="grdResultadoBusq_RowCommand" AllowPaging="True"
                                            PageSize="3" OnPageIndexChanging="grdResultadoBusq_PageIndexChanging">
                                            <Columns>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnSeleccionar" runat="server" ImageUrl="~/image/icons/icon_edit_p.png" CommandName="EDITAR"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idPaciente" HeaderText="ID" />                                                
                                                <asp:BoundField DataField="apellido1" HeaderText="Apell 1"/>
                                                <asp:BoundField DataField="apellido2" HeaderText="Apell 2"/>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                <asp:BoundField DataField="fechaNacim" HeaderText="Nacim" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="sexo" HeaderText="Sexo"/>
                                                
                                                
                                                
                                            </Columns>
                                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="WhiteSmoke" ForeColor="LightBlue"  HorizontalAlign="Center" CssClass="cssPager"/>
                                        </asp:GridView>
                                    </div>                            
                                 </div>
                        
                            </class>
                        </div>
                    </div>
                </asp:Panel>
                <!-- Column -->
            </div>

            <!-- Row -->     
            <div class="row" id="pnlDetalle">
                <!-- ============================================================== -->
                <!-- Inicia Columna de Información / Detalle de Pacientes -->
                <!-- ============================================================== -->
                <div class="col-lg-12 col-xlg-12 col-md-12">            
                    <div class="card">
                        <div class="card-block">
                            <class="m-t-30">
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-12" style="background-color:whitesmoke; border-radius:0px; height:20px;">
                                        <h5> Datos del Paciente</h5>
                                    </div>                                                   
                                </div>      
                                <div class="row text-left justify-content-md-start" style="margin-top:1em">
                                    <div class="col-2 col-form-label">
                                       <asp:Label ID="lblIdPac" runat="server" Text="ID"></asp:Label>
                                    </div>
                                    <div id="divTxtId" class="col-3">
                                        <asp:TextBox ID="txtIdPac" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblSexo" runat="server" Text="Sexo"></asp:Label>
                                    </div>                      
                                    <div class="col-4 form-check form-check-inline"" >
                                        <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal" CssClass="form-control-range rbl">
                                            <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Masc" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Otro" Value="O"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>                       
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblNomPac" runat="server" Text="Nombre"></asp:Label>
                                    </div>
                                    <div class="col-3 ">
                                        <asp:TextBox ID="txtNomPac" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblFecNacPac" runat="server" Text="F. Nac."></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtFechaNacim" runat="server" Text="" class="datepicker-here form-control form-control-sm" data-language="es" Enabled="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblApel1Pac" runat="server" Text="Apell 1"></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtApel1Pac" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>                           
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblApel2Pac" runat="server" Text="Apell 2"></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtApel2Pac" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div> 
                                    <div class="col-2 col-form-label">
                                    </div>
                                    <div class="col-3">
                                        <div class="row">
                                            <div class="col-3">                                        
                                            </div>
                                            <div class="col-1"></div>
                                            <div class="col-3">                                        
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- ============================================================== -->
                                <!-- Inicia Datos Adicionales / Datos de los Padres y Teléfonos === -->
                                <!-- ============================================================== -->
                        
                                <!-- ====================== Datos de Padres ======================= -->
                                <div class="row text-left justify-content-md-start" style="margin-top:0.5em">
                                    <div class="col-5" style="background-color:whitesmoke; border-radius:0px; height:20px;">
                                        <h5> Datos de la Madre</h5>
                                    </div>                            
                                    <div class="col-7" style="background-color:whitesmoke; border-radius:0px; height:20px">
                                        <h5> Datos del Padre</h5>
                                    </div>
                            
                                </div>                        
                                <div class="row text-left justify-content-md-start" style="margin-top:0.5em">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblIdMadre" runat="server" Text="ID"></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtIdMadre" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblIdPadre" runat="server" Text="ID"></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtIdPadre" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>                 
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblNomMadre" runat="server" Text="Nombre"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtNomMadre" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblNomPadre" runat="server" Text="Nombre"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtNomPadre" runat="server" Text="" class="form-control form-control-sm" data-language="es" Enabled="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblApel1Madre" runat="server" Text="Apell 1"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtApel1Madre" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblApel1Padre" runat="server" Text="Apell 1"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtApel1Padre" runat="server" Text="" class="form-control form-control-sm" data-language="es" Enabled="true"></asp:TextBox>
                                    </div>                        
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblApel2Madre" runat="server" Text="Apell 2"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtApel2Madre" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblApel2Padre" runat="server" Text="Apell 2"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtApel2Padre" runat="server" Text="" class="form-control form-control-sm" data-language="es" Enabled="true"></asp:TextBox>
                                    </div>                               
                                </div>
                                <!-- ====================== Medios de Contacto ======================= -->
                                <div class="row text-left justify-content-md-start" style="margin-top:0.5em">
                                    <div class="col-5" style="background-color:whitesmoke; border-radius:0px; height:20px;">
                                        <h5> Teléfonos de Contacto</h5>
                                    </div>                            
                                    <div class="col-7" style="background-color:whitesmoke; border-radius:0px; height:20px">                                
                                    </div>
                            
                                </div>                        
                                <div class="row text-left justify-content-md-start" style="margin-top:0.5em">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblTelCasa" runat="server" Text="Casa"></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtTelCasa" runat="server" type="number" CssClass="form-control form-control-sm phone-number" ></asp:TextBox>                                
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblTelOfi" runat="server" Text="Oficina"></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtTelOfi" runat="server" type="number" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>                 
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblTelUrg" runat="server" Text="Urgencia"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtTelUrg" runat="server" type="number" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-2 col-form-label">
                                        <asp:Label ID="lblTelCel" runat="server" Text="Celular"></asp:Label>
                                    </div>
                                    <div class="col-3 col-form-label">
                                        <asp:TextBox ID="txtTelCel" runat="server" type="number" class="form-control form-control-sm" data-language="es" Enabled="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row text-left justify-content-md-start">
                                    <div class="col-2 col-form-label">                                
                                    </div>
                                    <div class="col-3">                                
                                    </div> 
                                    <div class="col-0 col-form-label">
                                    </div>
                                    <div class="col-5">
                                        <div class="row">
                                            <div class="col-6 col-md-5">
                                                <asp:Button ID="btnGuardarPac" CssClass="btn btn-themecolor btn-sm btn-block" runat="server" Text="Guardar" OnClick="btnGuardarPac_Click" />
                                            </div>
                                            <div class="col-0"></div>
                                            <div class="col-6 col-md-5">
                                                <asp:Button ID="btnCancelar" CssClass="btn btn-warning btn-block btn-sm" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- ============================================================== -->
                                <!-- Finaliza Datos Adicionales -->
                                <!-- ============================================================== -->
                            </class>                            

                            <!-- ============================================================== -->
                            <!-- Finaliza Row de Teléfonos / Acordion -->
                            <!-- ============================================================== -->
                           
                        </div>
                    </div>
                </div>
                <!-- ============================================================== -->
                <!-- Fianliza Columna de Información / Detalle de Pacientes -->
                <!-- ============================================================== -->
            </div>
            <!-- ============================================================== -->
            <!-- Finaliza Row de Datos del Paciente -->
            <!-- ============================================================== -->

    

            <!-- ============================================================== -->
            <!-- Finaliza Page Content -->
            <!-- ============================================================== -->
        <uc1:mensajes ID="msgBox" runat="server" />
        <asp:HiddenField runat="server" ID="hdfBusquedaArcaMedisys" ClientIDMode="Static" /> 
        <asp:HiddenField runat="server" ID="hdfPacManual" ClientIDMode="Static" Value="NO" />        
        </ContentTemplate>
     </asp:UpdatePanel>
     
</asp:Content>
