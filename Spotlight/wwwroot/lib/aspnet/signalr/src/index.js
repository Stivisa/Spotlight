"use strict";
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
Object.defineProperty(exports, "__esModule", { value: true });
exports.JsonHubProtocol = exports.NullLogger = exports.TransferFormat = exports.HttpTransportType = exports.LogLevel = exports.MessageType = exports.HubConnectionBuilder = exports.HubConnectionState = exports.HubConnection = exports.DefaultHttpClient = exports.HttpResponse = exports.HttpClient = exports.TimeoutError = exports.HttpError = exports.AbortError = exports.VERSION = void 0;
// Version token that will be replaced by the prepack command
/** The version of the SignalR client. */
exports.VERSION = "0.0.0-DEV_BUILD";
var Errors_1 = require("./Errors");
Object.defineProperty(exports, "AbortError", { enumerable: true, get: function () { return Errors_1.AbortError; } });
Object.defineProperty(exports, "HttpError", { enumerable: true, get: function () { return Errors_1.HttpError; } });
Object.defineProperty(exports, "TimeoutError", { enumerable: true, get: function () { return Errors_1.TimeoutError; } });
var HttpClient_1 = require("./HttpClient");
Object.defineProperty(exports, "HttpClient", { enumerable: true, get: function () { return HttpClient_1.HttpClient; } });
Object.defineProperty(exports, "HttpResponse", { enumerable: true, get: function () { return HttpClient_1.HttpResponse; } });
var DefaultHttpClient_1 = require("./DefaultHttpClient");
Object.defineProperty(exports, "DefaultHttpClient", { enumerable: true, get: function () { return DefaultHttpClient_1.DefaultHttpClient; } });
var HubConnection_1 = require("./HubConnection");
Object.defineProperty(exports, "HubConnection", { enumerable: true, get: function () { return HubConnection_1.HubConnection; } });
Object.defineProperty(exports, "HubConnectionState", { enumerable: true, get: function () { return HubConnection_1.HubConnectionState; } });
var HubConnectionBuilder_1 = require("./HubConnectionBuilder");
Object.defineProperty(exports, "HubConnectionBuilder", { enumerable: true, get: function () { return HubConnectionBuilder_1.HubConnectionBuilder; } });
var IHubProtocol_1 = require("./IHubProtocol");
Object.defineProperty(exports, "MessageType", { enumerable: true, get: function () { return IHubProtocol_1.MessageType; } });
var ILogger_1 = require("./ILogger");
Object.defineProperty(exports, "LogLevel", { enumerable: true, get: function () { return ILogger_1.LogLevel; } });
var ITransport_1 = require("./ITransport");
Object.defineProperty(exports, "HttpTransportType", { enumerable: true, get: function () { return ITransport_1.HttpTransportType; } });
Object.defineProperty(exports, "TransferFormat", { enumerable: true, get: function () { return ITransport_1.TransferFormat; } });
var Loggers_1 = require("./Loggers");
Object.defineProperty(exports, "NullLogger", { enumerable: true, get: function () { return Loggers_1.NullLogger; } });
var JsonHubProtocol_1 = require("./JsonHubProtocol");
Object.defineProperty(exports, "JsonHubProtocol", { enumerable: true, get: function () { return JsonHubProtocol_1.JsonHubProtocol; } });
//# sourceMappingURL=index.js.map