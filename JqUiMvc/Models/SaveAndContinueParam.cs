using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    public class SaveAndContinueParam
    {
        /// <summary>
        /// The View calling.
        /// </summary>
        public string CurrView { get; set; }
        /// <summary>
        /// The calling View's state
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// If this is empty, S&C goes to the next sequenced step.
        /// Othewrwise, it goes to the this one.
        /// </summary>
        public string GoToView { get; set; }
    }
}
