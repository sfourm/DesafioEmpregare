#pragma checksum "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0520933fe0ac5a518404fddb61fae38dde3e6206"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuarios_Perfil), @"mvc.1.0.view", @"/Views/Usuarios/Perfil.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\_ViewImports.cshtml"
using Empregare;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\_ViewImports.cshtml"
using Empregare.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0520933fe0ac5a518404fddb61fae38dde3e6206", @"/Views/Usuarios/Perfil.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e427c0ee2d0e24ad10d361ee7316998e72a7a56d", @"/Views/_ViewImports.cshtml")]
    public class Views_Usuarios_Perfil : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Empregare.Models.Usuario>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Logout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
   ViewData["Title"] = "Perfil"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container"">
    <div id=""perfil"" class=""perfil"">
        <div class=""perfil-content"">
            <div class=""perfil-box"">
                <div class=""form-group title"">
                    <h1 class=""text-center"">Usuário logado</h1>
                    <h5 class=""text-center"">Bem vindo ");
#nullable restore
#line 10 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
                                                 Write(Model.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\n                </div>\n                <div class=\"form-group divSpaceMargin\">\n                    <label class=\"control-label\"><b>NOME: </b>");
#nullable restore
#line 13 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
                                                         Write(Model.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n                <div class=\"form-group divSpaceMargin\">\n                    <label class=\"control-label\"><b>TELEFONE: </b>");
#nullable restore
#line 16 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
                                                             Write(Model.Telefone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n                <div class=\"form-group divSpaceMargin\">\n                    <label class=\"control-label\"><b>EMAIL: </b>");
#nullable restore
#line 19 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
                                                          Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n\n                <div class=\"form-group divSpaceMargin text-center\">\n                    <button type=\"button\" data-id=\"");
#nullable restore
#line 23 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
                                              Write(Model.UsuarioId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"btn btn-info editar-senha\">Editar Senha</button>\n                    <button type=\"button\" data-id=\"");
#nullable restore
#line 24 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
                                              Write(Model.UsuarioId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"btn btn-info editar-perfil\">Editar Perfil</button>\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0520933fe0ac5a518404fddb61fae38dde3e62067136", async() => {
                WriteLiteral("Deslogar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </div>\n            </div>\n        </div>\n    </div>\n</div>\n\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n");
#nullable restore
#line 33 "C:\Users\Samuel\Desktop\DesafioEmpregare-9b91ad60b5f2e1a81fbd264566c0b2bff3e6c0d1\Empregare\Views\Usuarios\Perfil.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Empregare.Models.Usuario> Html { get; private set; }
    }
}
#pragma warning restore 1591
