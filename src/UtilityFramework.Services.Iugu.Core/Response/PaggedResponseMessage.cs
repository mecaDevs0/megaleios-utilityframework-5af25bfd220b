﻿using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Response
{
    public class PaggedResponseMessage<T> where T : class, new()
    {
        /// <summary>
        /// Total de items existentes
        /// </summary>
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        /// <summary>
        /// Total de items retornados
        /// </summary>
        public int TotalItemsReturned => Items.Length;

        /// <summary>
        /// Agência da Conta
        /// Formatos com validação automática(9999-D, 9999)
        /// </summary>
        [JsonProperty("Items")]
        public T[] Items { get; set; }
    }
}
