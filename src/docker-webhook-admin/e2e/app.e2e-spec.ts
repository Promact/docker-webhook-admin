import { DockerWebhookAdminPage } from './app.po';

describe('docker-webhook-admin App', () => {
  let page: DockerWebhookAdminPage;

  beforeEach(() => {
    page = new DockerWebhookAdminPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
