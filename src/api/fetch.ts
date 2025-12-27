export async function apiFetch<T = unknown>(path: string) {
  const response = await fetch(`https://localhost:7072${path}`);

  const text = await response.text();

  try {
    const json = JSON.parse(text);
    if (!response.ok) {
      throw new Error(
        `The call to ${response.url} returned ${response.status} ${response.statusText}`,
        { cause: json }
      );
    }

    return json as T;
  } catch (e) {
    if (e instanceof SyntaxError) {
      throw new Error(
        `The call to ${response.url} returned ${response.status} ${response.statusText}, but the response is not valid JSON. Response: "${text}"`,
        { cause: e }
      );
    } else {
      throw new Error(
        `The call to ${response.url} returned ${response.status} ${response.statusText}, but an unknown error was caught while processing the response. Response: "${text}"`,
        { cause: e }
      );
    }
  }
}
