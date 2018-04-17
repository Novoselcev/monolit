using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monolit.Models
{
    [Table("Messager", Schema = "u7716449_vxod")]
    public class Messager
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

 
        [Display(Name = "Телефон")]
        public string Phone { get; set; }


        [Display(Name = "Как к Вам обращаться")]
        public string UserName { get; set; }


        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Неверный формат электронной почты")]
        [Display(Name = "Email")]
        public string EmailAdrress { get; set; }


        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст сообщения")]
        public string Message { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }

    public class ContactSimple
    {
        
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Как к Вам обращаться")]
        public string UserName { get; set; }

       
        [StringLength(50)]
        [Display(Name = "Email")]
        public string EmailAdrress { get; set; }

    }

}