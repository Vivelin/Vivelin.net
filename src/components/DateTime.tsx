export function DateTime(props: { children: Date | undefined }) {
  return (
    <time dateTime={props.children?.toISOString()}>
      {props.children?.toLocaleString("en-US", {
        dateStyle: "long",
        timeStyle: "short",
        hour12: false,
      })}
    </time>
  );
}
