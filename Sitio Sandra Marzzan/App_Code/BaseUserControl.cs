using System.Web.UI;
using System.Reflection;
using System;
using System.Text;


    /// <summary>
    /// Summary description for BaseControl
    /// </summary>
    public abstract class BaseUserControl : UserControl
    {
        

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterUserControlWebMethods();
        }

        /// <summary>
        /// Registers the user control web methods tagged with the UserControlWebMethodAttribute
        /// </summary>
        private void RegisterUserControlWebMethods()
        {
            foreach (MethodInfo method in this.GetType().GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
                if (method.GetCustomAttributes(typeof(UserControlScriptMethodAttribute), true).Length > 0)
                    RegisterUserControlWebMethod(method);

            Type baseType = this.GetType().BaseType;
            if (baseType != null && (baseType.Namespace == null || !baseType.Namespace.StartsWith("System")))
                foreach (MethodInfo method in baseType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
                    if (method.GetCustomAttributes(typeof(UserControlScriptMethodAttribute), true).Length > 0)
                        RegisterUserControlWebMethod(method);
        }

        /// <summary>
        /// Registers a user control web method based on the methodInfo signature through reflection.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <remarks>AJAX Voodoo!</remarks>
        private void RegisterUserControlWebMethod(MethodInfo method)
        {
            string blockName = string.Concat(method.Name, "_webMethod_uc");

            StringBuilder funcBuilder = new StringBuilder();
            funcBuilder.Append("function ");
            funcBuilder.Append(method.Name);
            funcBuilder.Append("(successCallback,failureCallback");
            foreach (var par in method.GetParameters())
                funcBuilder.AppendFormat(",{0}", par.Name);
            funcBuilder.Append("){if(PageMethods.PageServiceRequest){try{var parms=[];for(var i=2;i<arguments.length;i++){parms.push(arguments[i]);}PageMethods.PageServiceRequest(");
            funcBuilder.AppendFormat("'{0}','{1}'", method.DeclaringType.AssemblyQualifiedName, method.Name);
            funcBuilder.Append(",parms,successCallback,failureCallback);}catch(e){alert(e.toString());}}}");

            ScriptManager.RegisterClientScriptBlock(this, GetType(), blockName, funcBuilder.ToString(), true);
        }
    }
