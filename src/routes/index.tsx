import { Title } from "@solidjs/meta";
import { Match, Switch } from "solid-js";
import Counter from "~/components/Counter";
import { Possibility, Random } from "~/components/Random";

function A() {
  console.log("A");
  return <>A</>;
}

function B() {
  console.log("B");
  return <>B</>;
}

function C() {
  console.log("C");
  return <>C</>;
}

export default function Home() {
  return (
    <main>
      <Title>Hello World</Title>
      <h1>Hello world!</h1>
      <Random>
        <Possibility>
          <A />
        </Possibility>
        <Possibility>
          <B />
        </Possibility>
        <Possibility>
          <C />
        </Possibility>
      </Random>
    </main>
  );
}
