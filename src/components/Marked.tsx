import { marked } from "marked";
import { createResource } from "solid-js";

export function Marked(props: { content: string | undefined }) {
  const [content] = createResource(
    () => props.content,
    (data) => {
      return data ? marked.parse(data, { async: true }) : undefined;
    }
  );

  return <div class="markdown-content" innerHTML={content()} />;
}
