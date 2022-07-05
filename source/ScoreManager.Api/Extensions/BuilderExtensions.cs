using ScoreManager.Data;

namespace ScoreManager.Extensions
{
    public static class BuilderExtensions
    {
        public static void ConfigureSelfBindableEntities(this IServiceCollection services)
        {
            var type = typeof(CrudBase<>);
            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => p.IsClass && p.FullName.Contains("ScoreManager") && p.Name.EndsWith("Service"));
            foreach (var t in types)
            {
                var @interface = t.GetInterfaces().Single(w => !w.Name.Contains("Base"));
                services.AddTransient(@interface, t);
            }
        }
    }
}