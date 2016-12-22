namespace Wallpaper10CnC
{
    using System;
    using System.ComponentModel;

    public static class Helper
    {
        public static string GetDescription(this Enum arbitraryEnum)
        {
            if (arbitraryEnum == null)
            {
                return null;
            }

            var attribute = GetAttribute<DescriptionAttribute>(arbitraryEnum);

            return attribute == null ? null : attribute.Description;
        }

        private static T GetAttribute<T>(Enum arbitraryEnum) where T : class
        {
            var enumType = arbitraryEnum.GetType();
            var memberInfo = enumType.GetMember(arbitraryEnum.ToString());

            if (memberInfo.Length <= 0)
            {
                return null;
            }

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
            {
                return (T)attributes[0];
            }

            return null;
        }
    }
}
