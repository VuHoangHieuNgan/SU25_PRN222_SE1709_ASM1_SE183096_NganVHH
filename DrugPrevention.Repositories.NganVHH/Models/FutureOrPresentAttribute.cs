using System;
using System.ComponentModel.DataAnnotations;

namespace DrugPrevention.Repositories.NganVHH.Models
{
    /// <summary>
    /// Attribute kiểm tra ngày giờ phải là hiện tại hoặc tương lai (không cho phép quá khứ)
    /// </summary>
    public class FutureOrPresentAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true; // Để [Required] xử lý trường hợp null
            if (value is DateTime dt)
            {
                // So sánh với thời gian hiện tại (UTC)
                return dt >= DateTime.UtcNow;
            }
            return false;
        }
    }
} 