export function shortenUrl(urlToShorten: string) {
  return fetch("https://localhost:7231/api/v1/shorten?url=" + urlToShorten, {
    method: "POST",
  }).then((response) => {
    if (!response.ok) {
      throw new Error("Failed to shorten URL");
    }
    return response.json();
  });
}
