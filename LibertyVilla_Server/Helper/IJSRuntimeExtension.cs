﻿using Microsoft.JSInterop;

namespace LibertyVilla_Server.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }

        public static async ValueTask LogOut(this IJSRuntime JSRuntime)
        {
            await JSRuntime.InvokeVoidAsync("Logout");
        }
    }
}
