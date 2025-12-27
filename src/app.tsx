import { MetaProvider, Title } from "@solidjs/meta";
import { Router, RouteSectionProps } from "@solidjs/router";
import { FileRoutes } from "@solidjs/start/router";
import { ErrorBoundary, Suspense } from "solid-js";
import "./app.css";
import { Nav } from "./components/Nav";
import { RandomQuote } from "./components/quotes/RandomQuote";

export default function App() {
  return (
    <Router root={Layout}>
      <FileRoutes />
    </Router>
  );
}

function Layout(props: RouteSectionProps) {
  return (
    <MetaProvider>
      <Title>Vivelin.net</Title>
      <header>
        <Nav />
      </header>
      <ErrorBoundary fallback={<>???</>}>
        <Suspense>{props.children}</Suspense>
      </ErrorBoundary>
      <footer>
        <RandomQuote />
      </footer>
    </MetaProvider>
  );
}
