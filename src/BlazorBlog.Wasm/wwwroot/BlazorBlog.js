class BlazorBlogJs {
    #inFade = 0.00;
    #outFade = 1.00;
    #viewWidth = 0;

    startup() {
        this.#fadeIn(`header, main, footer`);
        return;

        setTimeout(() => {
            this.#fadeOut(`main, footer`);
        }, 2000);
    }

    getViewWidth() {
        if (this.#viewWidth === 0) {
            const header = document.querySelector(`header`);
            this.#viewWidth = parseInt(header.offsetWidth, 10);
        }
        return this.#viewWidth;
    }

    resize() {
        this.#viewWidth = 0;
        const viewWidth = this.getViewWidth();
        document.querySelectorAll(`[data-resize]`).forEach(el => {
            const resizeData = JSON.parse(el.getAttribute(`data-resize`));
            const maxWidth = resizeData.MaxWidthViewRatio * viewWidth
            const ratio = maxWidth / resizeData.Width;
            el.setAttribute(`width`, Math.round(resizeData.Width * ratio));
            el.setAttribute(`height`, Math.round(resizeData.Height * ratio));
        });
    }

    #updateOpacity(qs, opacity) {
        document.querySelectorAll(qs).forEach(el => {
            el.style.opacity = opacity;
        });
    }

    #fadeIn(qs) {
        this.#updateOpacity(qs, this.#inFade);
        if (this.#inFade < 1) {
            this.#inFade += 0.1;
            requestAnimationFrame(() => this.#fadeIn(qs));
            return;
        }
        this.#inFade = 1;
        this.#updateOpacity(qs, this.#inFade);
        this.#inFade = 0;
    }

    #fadeOut(qs) {
        this.#updateOpacity(qs, this.#outFade);
        if (this.#outFade > 0) {
            this.#outFade -= 0.1;
            requestAnimationFrame(() => this.#fadeOut(qs));
            return;
        }
        this.#outFade = 0;
        this.#updateOpacity(qs, this.#outFade);
        this.#outFade = 1;
    }
}

var bbjs = new BlazorBlogJs();
window.addEventListener(`resize`, () => { bbjs.resize(); });

