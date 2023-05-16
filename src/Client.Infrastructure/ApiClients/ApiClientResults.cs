using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Article;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourArticle;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourDtos;
using Grs.BioRestock.Transfer.DataModels.Client;
using System;
using System.Collections.Generic;

namespace Grs.BioRestock.Client.Infrastructure.ApiClients
{
    public partial class BooleanIResult : Result<bool>
    {
    }

    public partial class BooleanResult : Result<bool>
    {
    }

    public partial class Int32Result : Result<Int32>
    {
    }

    public partial class Int32IResult : Result<Int32>
    {
    }

    public partial class StringResult : Result<string>
    {
    }

    public partial class StringIResult : Result<string>
    {
    }

    public partial class StringListIResult : Result<List<string>>
    {
    }
    public partial class BonDeRetourDtoListResult : Result<List<BonDeRetourDto>>
    {
    }
    public partial class BonDeRetourDtoResult : Result<BonDeRetourDto>
    {
    }
    public partial class GetArticleDtoListResult : Result<ArticleDto>
    {
    }
    public partial class CustomerListResult : Result<CustomerDto>
    {
    }
    public partial class ArticleDtoListResult : Result<List<ArticleDto>>
    {
    }
    public partial class BonDeRetourArticleDtoListResult : Result<List<BonDeRetourArticleDto>>
    {
    }
    public partial class CustomerDtoListResult : Result<List<CustomerDto>>
    {
    }
}