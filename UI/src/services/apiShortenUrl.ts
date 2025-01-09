export interface ShortenedUrl {
  shortCode: string;
  originalUrl: string;
  createdAtUtc: Date;
}

export function shortenUrl(urlToShorten: string) {
  const params = { url: urlToShorten };
  const options = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };
  return fetch(
    "/api/v1/shorten?" + new URLSearchParams(params).toString(),
    options,
  ).then((response) => {
    if (!response.ok) {
      throw new Error("Failed to shorten URL");
    }
    return response;
  });
}

export function getUrls(): Promise<ShortenedUrl[]> {
  const options = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };
  return fetch("/api/v1/urls", options).then((response) => {
    if (!response.ok) {
      throw new Error("Failed to shorten URL");
    }
    return response.json();
  });
}
