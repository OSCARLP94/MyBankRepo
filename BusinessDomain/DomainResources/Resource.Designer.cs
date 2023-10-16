﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessDomain.DomainResources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BusinessDomain.DomainResources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El cliente no se halla o no existe.
        /// </summary>
        internal static string ClientNotExists {
            get {
                return ResourceManager.GetString("ClientNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La fecha efectiva debe ser igual o mayor a fecha actual.
        /// </summary>
        internal static string EffectDateMayorCurrentDate {
            get {
                return ResourceManager.GetString("EffectDateMayorCurrentDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No tiene fondos suficientes.
        /// </summary>
        internal static string NotEnoughtFunds {
            get {
                return ResourceManager.GetString("NotEnoughtFunds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El valor de la transacción no supera el monto minimo ({0}).
        /// </summary>
        internal static string NotMinValueTransaction {
            get {
                return ResourceManager.GetString("NotMinValueTransaction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La transacción no permite producto origen y destino iguales.
        /// </summary>
        internal static string NotOriginAndDestinyEquals {
            get {
                return ResourceManager.GetString("NotOriginAndDestinyEquals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No existen productos para el cliente solicitado.
        /// </summary>
        internal static string NotProductsClient {
            get {
                return ResourceManager.GetString("NotProductsClient", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Producto {0} no tiene fondos suficientes.
        /// </summary>
        internal static string ProductNotEnoughtFunds {
            get {
                return ResourceManager.GetString("ProductNotEnoughtFunds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El producto ({0}) no se halla o no existe.
        /// </summary>
        internal static string ProductNotExists {
            get {
                return ResourceManager.GetString("ProductNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Error.
        /// </summary>
        internal static string TransactionError {
            get {
                return ResourceManager.GetString("TransactionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tipo de transacción requiere producto origen.
        /// </summary>
        internal static string TransactionRequireDestinyProduct {
            get {
                return ResourceManager.GetString("TransactionRequireDestinyProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tipo de transacción requiere producto origen.
        /// </summary>
        internal static string TransactionRequireOriginProduct {
            get {
                return ResourceManager.GetString("TransactionRequireOriginProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Cualquier transaccion requiere producto destino u producto origen.
        /// </summary>
        internal static string TransactionsNeedProduct {
            get {
                return ResourceManager.GetString("TransactionsNeedProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Exitoso.
        /// </summary>
        internal static string TransactionSuccess {
            get {
                return ResourceManager.GetString("TransactionSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Los tipos de transacciones no se hallan o no existen.
        /// </summary>
        internal static string TypeTransactionsNotExists {
            get {
                return ResourceManager.GetString("TypeTransactionsNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Transacciones requieren valores mayores a 0.
        /// </summary>
        internal static string ZeroNotAllowTransaction {
            get {
                return ResourceManager.GetString("ZeroNotAllowTransaction", resourceCulture);
            }
        }
    }
}