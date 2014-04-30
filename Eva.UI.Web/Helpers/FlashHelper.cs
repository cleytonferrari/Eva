namespace System.Web.Mvc
{
    public enum FlashEnum
    {
        Success = 1,
        Info = 2,
        Warning = 3,
        Error = 4
    }
    public static class FlashHelper
    {
        public static void Flash(this Controller controller, string message, FlashEnum type = FlashEnum.Success)
        {
            var classe = "";
            switch (type)
            {
                case FlashEnum.Success: classe = "alert-success";
                    break;
                case FlashEnum.Info: classe = "alert-info";
                    break;
                case FlashEnum.Warning: classe = "alert-warning";
                    break;
                case FlashEnum.Error: classe = "alert-danger";
                    break;
            }
            controller.TempData[classe] = message;
        }
    }
}