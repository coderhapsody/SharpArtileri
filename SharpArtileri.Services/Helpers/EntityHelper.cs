using System;

namespace SharpArtileri.Services.Helpers
{
    public static class EntityHelper
    {
        public static void SetAuditFields(int rowID, dynamic entity, string userName)
        {
            try
            {
                if (rowID == 0)
                {
                    entity.CreatedWhen = DateTime.Now;
                    entity.CreatedWho = userName;
                }
                entity.ChangedWhen = DateTime.Now;
                entity.ChangedWho = userName;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }
        }

        public static void SetAuditFieldsForInsert(dynamic entity, string userName)
        {
            try
            {
                entity.CreatedWhen = DateTime.Now;
                entity.CreatedWho = userName;
                entity.ChangedWhen = DateTime.Now;
                entity.ChangedWho = userName;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }
        }

        public static void SetAuditFieldsForUpdate(dynamic entity, string userName)
        {
            try
            {
                entity.ChangedWhen = DateTime.Now;
                entity.ChangedWho = userName;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }
        }
    }
}
