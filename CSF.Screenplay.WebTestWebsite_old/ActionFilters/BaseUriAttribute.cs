﻿//
// BaseUrlAttribute.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Web.Mvc;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.ActionFilters
{
  public class BaseUriAttribute : ActionFilterAttribute
  {
    const string DefaultBaseUri = "http://localhost:8080/";

    readonly string baseUri;

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
      var viewResult = GetViewResult(filterContext);
      if(viewResult == null) return;

      var baseModel = GetBaseModel(viewResult);
      if(baseModel == null) return;

      baseModel.BaseUri = new Uri(baseUri);

      base.OnResultExecuting(filterContext);
    }

    ViewResultBase GetViewResult(ResultExecutingContext filterContext)
      => filterContext?.Result as ViewResultBase;

    ModelBase GetBaseModel(ViewResultBase viewResult)
      => viewResult.Model as ModelBase;

    public BaseUriAttribute(string baseUri)
    {
      if(baseUri == null)
        throw new ArgumentNullException(nameof(baseUri));

      this.baseUri = baseUri;
    }

    public BaseUriAttribute()
    {
      baseUri = DefaultBaseUri;
    }
  }
}
