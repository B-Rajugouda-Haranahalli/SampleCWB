// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace CWB.Identity
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}