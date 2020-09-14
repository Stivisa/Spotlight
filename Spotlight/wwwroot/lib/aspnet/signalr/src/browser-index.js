"use strict";
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __exportStar = (this && this.__exportStar) || function(m, exports) {
    for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(exports, p)) __createBinding(exports, m, p);
};
Object.defineProperty(exports, "__esModule", { value: true });
// This is where we add any polyfills we'll need for the browser. It is the entry module for browser-specific builds.
require("es6-promise/dist/es6-promise.auto.js");
// Copy from Array.prototype into Uint8Array to polyfill on IE. It's OK because the implementations of indexOf and slice use properties
// that exist on Uint8Array with the same name, and JavaScript is magic.
// We make them 'writable' because the Buffer polyfill messes with it as well.
if (!Uint8Array.prototype.indexOf) {
    Object.defineProperty(Uint8Array.prototype, "indexOf", {
        value: Array.prototype.indexOf,
        writable: true,
    });
}
if (!Uint8Array.prototype.slice) {
    Object.defineProperty(Uint8Array.prototype, "slice", {
        value: Array.prototype.slice,
        writable: true,
    });
}
if (!Uint8Array.prototype.forEach) {
    Object.defineProperty(Uint8Array.prototype, "forEach", {
        value: Array.prototype.forEach,
        writable: true,
    });
}
__exportStar(require("./index"), exports);
//# sourceMappingURL=browser-index.js.map