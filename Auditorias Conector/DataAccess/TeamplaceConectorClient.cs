namespace Auditorias_Conector.DataAccess
{
    public class TeamplaceConnectorClient
    {
        private HttpClient teamplaceClient = new HttpClient();

        public TeamplaceConnectorClient(IHttpClientFactory clientFactory)
        {
            teamplaceClient = clientFactory.CreateClient("teamplace");
        }
    }
}
