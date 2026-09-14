using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Domain.Exception
{
    /// <summary>
    /// Base exception cho toàn bộ custom exception trong hệ thống.
    /// Middleware sẽ dựa vào kiểu con của lớp này để map ra đúng HTTP status code.
    /// </summary>
    public abstract class AppException : System.Exception
    {
        protected AppException(string message) : base(message) { }
    }

    /// <summary>Dùng khi không tìm thấy resource -> trả về 404</summary>
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entityName, object key)
            : base($"{entityName} với id '{key}' không tồn tại.") { }
    }

    /// <summary>Dùng khi dữ liệu đầu vào không hợp lệ -> trả về 400</summary>
    public class ValidationAppException : AppException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationAppException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationAppException(IDictionary<string, string[]> errors)
            : base("Một hoặc nhiều trường dữ liệu không hợp lệ.")
        {
            Errors = errors;
        }
    }

    /// <summary>Dùng khi request hợp lệ về mặt cú pháp nhưng vi phạm nghiệp vụ -> trả về 400</summary>
    public class BusinessRuleException : AppException
    {
        public BusinessRuleException(string message) : base(message) { }
    }

    /// <summary>Dùng khi user không có quyền thực hiện hành động -> trả về 403</summary>
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message = "Bạn không có quyền thực hiện hành động này.")
            : base(message) { }
    }

    public class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(message) { }
    }

    /// <summary>Dùng khi có xung đột dữ liệu, VD: SKU đã tồn tại, email đã đăng ký -> trả về 409</summary>
    public class ConflictException : AppException
    {
        public ConflictException(string message) : base(message) { }
    }
}
