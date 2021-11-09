import { GroupOfIssues } from './types';
export class BffApi {
  baseUri: string;

  constructor(baseUri: string) {
    this.baseUri = baseUri;
  }

  async GetAllGroupsOfIssues(): Promise<GroupOfIssues[]> {
    return await this.getJson(new URL('groupofissue', this.baseUri));
  }

  async getJson(url: URL): Promise<any> {
    const response = await this.sendAsync(url, 'GET');
    if (!response.ok) {
      throw new Error(`${response.status}: ${response.statusText}`);
    }

    return response.json();
  }

  async sendAsync(
    url: URL,
    method: string,
    headers: Headers = new Headers(),
    body?: BodyInit | null
  ): Promise<Response> {
    //const token = "hereGettingToken"
    //headers.set("Authorization", `Bearer ${token}`)
    return fetch(url.toString(), {
      method: method,
      headers: headers,
      body: body,
    });
  }
}
