//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartDevices.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Statuses
    {
        public Statuses()
        {
            this.Devices = new HashSet<Devices>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Devices> Devices { get; set; }
    }
}
