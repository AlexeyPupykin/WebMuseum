//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Museum.Models
{
    using System;
    
    public partial class GetAllExhibits_Result
    {
        public int IDExhibit { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DateOfCreation { get; set; }
        public string Creator { get; set; }
        public string Style { get; set; }
        public string Type { get; set; }
        public Nullable<int> Room { get; set; }
        public Nullable<int> Exhibition { get; set; }
    }
}
