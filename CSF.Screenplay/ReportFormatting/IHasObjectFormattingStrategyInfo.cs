//
// IGetsObjectFormattingPriority.cs
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
namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// A specialisation of <see cref="IFormatsObjectForReport"/> which provides metadata allowing the formatter to
  /// participate in a strategy-pattern selection process.
  /// </summary>
  public interface IHasObjectFormattingStrategyInfo : IFormatsObjectForReport
  {
    /// <summary>
    /// Gets the priority by which this formatter should be used for formatting the given object.
    /// A higher numeric priority means that the formatter is more likely to be selected, where a lower priority
    /// means it is less likely.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This value is meaningless if <see cref="CanFormat(object)"/> returns <c>false</c> for the same object.
    /// </para>
    /// </remarks>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">The object to be formatted.</param>
    int GetFormattingPriority(object obj);

    /// <summary>
    /// Gets a value which indicates whether or not the current instance is capable of providing a format for the given
    /// object.  If it is <c>false</c> then this formatter must not be used for that object.
    /// </summary>
    /// <returns><c>true</c>, if the current instance can format the given object, <c>false</c> otherwise.</returns>
    /// <param name="obj">The object to be formatted.</param>
    bool CanFormat(object obj);
  }
}
