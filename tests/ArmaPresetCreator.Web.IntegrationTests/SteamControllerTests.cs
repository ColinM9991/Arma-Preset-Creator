using ArmaPresetCreator.Web.Models;
using System.Threading.Tasks;
using Xunit;

#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
namespace ArmaPresetCreator.Web.IntegrationTests
{
    public class SteamControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory customWebApplicationFactory;

        public SteamControllerTests(CustomWebApplicationFactory customWebApplicationFactory)
        {
            this.customWebApplicationFactory = customWebApplicationFactory;
        }

        [Fact]
        public async Task GetPublishedItem_WithModContainingNoDependencies_ReturnsNoChildItems()
        {
            // Arrange
            var client = customWebApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/steam/workshop/publisheditems/1584366636");
            var responseContent = await response.Content.ReadAs<SteamWorkshopItem>();

            // Assert
            Assert.Null(responseContent.Items);
        }

        [Theory(DisplayName = "Getting Published Item with scenario:")]
        [InlineData("contains only top level children (none nested)", 1203529005, 1)]
        [InlineData("contains children with nested children.", 410126510, 2)]
        [InlineData("contains children with nested duplicate children.", 4101265101, 2)]
        public async Task GetPublishedItem_WithSpecifiedScenario_MeetsCriteria(string testCaseScenario, long publishedItemId, int expectedNumberOfItems)
        {
            // Arrange
            var client = customWebApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/steam/workshop/publisheditems/{publishedItemId}");
            var responseContent = await response.Content.ReadAs<SteamWorkshopItem>();

            // Assert
            Assert.NotNull(responseContent.Items);
            Assert.Equal(expectedNumberOfItems, responseContent.Items.Length);
        }
    }
}
