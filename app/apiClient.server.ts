export async function sendApiRequest<T>(
    httpMethod: string,
    endpoint: string | URL,
    options?: Partial<RequestInit>,
): Promise<T | undefined> {
    const apiUrl = new URL(endpoint, process.env.API_URL);
    const response = await fetch(apiUrl, {
        method: httpMethod,
        ...options,
    });

    if (response.status === 404) {
        throw new Response('Not Found', { status: 404 });
    } else if (!response.ok) {
        throw new Response(response.statusText, {
            status: 502,
        });
    }

    const content = await response.text();
    if (!content) {
        return undefined;
    }

    return JSON.parse(content) as T;
}
