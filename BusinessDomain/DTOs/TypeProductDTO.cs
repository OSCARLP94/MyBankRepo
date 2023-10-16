﻿
namespace BusinessDomain.DTOs
{
    public class TypeProductDTO
    {

        /// <summary>
        /// Codigo
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indiica si puede o no usarse en asignaciones
        /// </summary>
        public bool? IsEnabled { get; set; }
    }
}
