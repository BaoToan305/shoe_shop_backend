using NUlid;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace shoe_shop_backend.Application.Helper
{
    public class UnityHelper
    {

        private static readonly ConcurrentDictionary<(Type, Type), Action<object, object>> _copyCache = new();

        public static void CopyProperties(object source, object target)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));

            // [SỬA] Chỉ phân tích + biên dịch lần đầu cho mỗi cặp kiểu
            var copier = _copyCache.GetOrAdd((source.GetType(), target.GetType()), key =>
            {
                var (srcType, tgtType) = key;
                var flags = BindingFlags.Public | BindingFlags.Instance;

                var s = Expression.Parameter(typeof(object), "s");
                var t = Expression.Parameter(typeof(object), "t");
                var src = Expression.Convert(s, srcType);
                var tgt = Expression.Convert(t, tgtType);

                var targetProps = tgtType.GetProperties(flags)
                    .Where(p => p.CanWrite && p.GetIndexParameters().Length == 0)
                    .ToDictionary(p => p.Name);

                var body = new List<Expression>();

                foreach (var sp in srcType.GetProperties(flags))
                {
                    if (!sp.CanRead || sp.GetIndexParameters().Length > 0) continue;
                    if (!targetProps.TryGetValue(sp.Name, out var tp)) continue;

                    var srcProp = Expression.Property(src, sp);
                    var tgtProp = Expression.Property(tgt, tp);

                    if (tp.PropertyType.IsAssignableFrom(sp.PropertyType))
                    {
                        // Cùng kiểu, hoặc T -> T?, hoặc kế thừa: gán thẳng
                        body.Add(Expression.Assign(tgtProp, Expression.Convert(srcProp, tp.PropertyType)));
                    }
                    else if (Nullable.GetUnderlyingType(sp.PropertyType) == tp.PropertyType)
                    {
                        // T? -> T: chỉ gán khi nguồn có giá trị (giống hành vi bản cũ)
                        body.Add(Expression.IfThen(
                            Expression.Property(srcProp, "HasValue"),
                            Expression.Assign(tgtProp, Expression.Property(srcProp, "Value"))));
                    }
                    // Kiểu khác không tương thích: bỏ qua
                }

                if (body.Count == 0) body.Add(Expression.Empty());

                return Expression.Lambda<Action<object, object>>(Expression.Block(typeof(void), body), s, t).Compile();
            });

            copier(source, target); // [SỬA] Gọi delegate đã cache, không còn Reflection
        }

        public static string GenerateUlid()
        {
            return Ulid.NewUlid().ToString();
        }


    }
}
