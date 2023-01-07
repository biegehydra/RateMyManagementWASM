using Microsoft.AspNetCore.Components;
using RateMyManagementWASM.Client.Paginiation;

namespace RateMyManagementWASM.Client.Shared
{
    public partial class Paginiation
    {
        [Parameter] public PagingMetaData MetaData { get; set; }
        [Parameter] public int Spread { get; set; }
        [Parameter] public EventCallback<string> SelectedPage { get; set; }
        
        private List<PagingLink> _links;
        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }
        private void CreatePaginationLinks()
        {
            _links = new List<PagingLink>();
            _links.Add(new PagingLink(MetaData.CurrentPage - 1, MetaData.HasPrevious, "Previous"));
            int i = 1;
            for (char c = 'A'; c <= 'Z'; c++, i++)
            {
                _links.Add(new PagingLink(i, true, c.ToString()) { Active = MetaData.CurrentPage == i });
            }
            _links.Add(new PagingLink(MetaData.CurrentPage + 1, MetaData.HasNext, "Next"));
        }
        private async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == MetaData.CurrentPage || !link.Enabled)
                return;
            MetaData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Text);
        }
    }
}
