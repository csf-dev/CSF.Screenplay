//
// Matches.cs
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
namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builder type for creating matchers.
  /// </summary>
  public static class Matches
  {
    /// <summary>
    /// Gets a matcher builder, for creating a matcher from queries.
    /// </summary>
    /// <value>The matcher builder.</value>
    public static IQueryBasedMatcherBuilder Query => new QueryBuilder();

    /// <summary>
    /// Gets a matcher builder, for creating a matcher for a specific criterion.
    /// </summary>
    /// <value>The matcher builder.</value>
    public static ICriteriaBasedMatcherBuilder Criteria => new QueryBuilder();

    /// <summary>
    /// Helper interface (it has no functionality) for the building of matchers which are based on queries.
    /// </summary>
    public interface IQueryBasedMatcherBuilder {}

    /// <summary>
    /// Helper interface (it has no functionality) for the building of matchers which are based on criteria.
    /// </summary>
    public interface ICriteriaBasedMatcherBuilder {}

    class QueryBuilder : IQueryBasedMatcherBuilder, ICriteriaBasedMatcherBuilder {}
  }
}
