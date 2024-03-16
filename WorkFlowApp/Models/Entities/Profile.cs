using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowApp.Models.Entities
{
    public class Profile:BaseEntity
    {
        [Required(ErrorMessage = "يرجى إدخال اسم المستخدم.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "عدد حروف الاسم يجب ان يكون بين 2 الى 50 حرف")]
        public string DisplayName { get; set; } = string.Empty;



        [Required(ErrorMessage = "يرجى إدخال اسم الوصف.")]
        [StringLength(200, ErrorMessage = "لايمكن ادخال اكتر من 200 حرف")]
        public string bio { get; set; } = string.Empty;



        [Required(ErrorMessage = "يرجى إدخال رقم الهاتف.")]
        [RegularExpression(@"^(092|091|094|095)\d{7}$", ErrorMessage = "يجب ان يبدا رقم الهاتف ب  092, 091, 094, او 095 ويتكون من عشره ارقام")]

        public string PhoneNum { get; set; }=string.Empty;

        [Required(ErrorMessage = "يرجى إختيار الصورة.")]

        public string Pic { get; set; }=string.Empty;



        public bool Gender { get; set; }
        


        public string UserId { get; set; } = string.Empty;

        public ApplicationUser user { get; set; }


    }
}
