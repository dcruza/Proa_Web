<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mensajes.ascx.cs" Inherits="Proa_Web.UserControl.mensajes" %>

<link href='<%= Page.ResolveUrl("~/css/msgBoxLight.css") %>' rel="stylesheet" />
<script src='<%= Page.ResolveUrl("~/js/jquery.msgBox.js") %>'></script>
<%--<script src=<%= Page.ResolveUrl("~/Scripts/jquery-ui.js") %>></script>--%>
<script type="text/javascript">
    //// Muestra un calendario
    //$(document).ready(function () {
    //    $('.datepicker-field').datepicker();
    //});
   

    // Muestra los mensaje de éxito, error y alertas
    function msgBox(title, val, type) {
        $.msgBox({
            title: title,
            content: val,
            type: type
        });
    }

    function confirmBox(title, val, confValueYes) {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";

        $.msgBox({
            title: title,
            content: val,
            type: "confirm",
            buttons: [{ value: "Sí" }, { value: "No" }],
            success: function (result) {
                if (result == "Sí") {
                    
                    confirm_value.value = confValueYes;                    
                    document.getElementById("frm").appendChild(confirm_value);
                    document.getElementById("frm").submit();
                }
                
            }
        });
    }
</script>




