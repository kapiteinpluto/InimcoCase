<template>
  <div class="col fieldGroup">
    <label class="fancy-tag">
      {{ this.inputName }}
    </label>
    <input
      class="inputField"
      v-model="value"
      type="text"
      @input="validateText"
    />
    <p v-if="error" class="error">{{ error }}</p>
  </div>
</template>

<script>
export default {
  props: ["modelValue", "inputName"],
  emits: ["update:modelValue"],
  computed: {
    value: {
      get() {
        return this.modelValue;
      },
      set(newValue) {
        this.$emit("update:modelValue", newValue);
      },
    },
  },
  data() {
    return {
      error: null,
    };
  },
  methods: {
    validateText() {
      const validCharacters = /^[A-Za-z]+$/;
      if (!this.value) {
        this.error = this.inputName + " is required.";
        this.$root.$refs.form.triggerError(this.inputName);
      } else if (this.value.length < 2 || this.value.length > 50) {
        this.error = this.inputName + " must be between 2 and 50 characters.";
        this.$root.$refs.form.triggerError(this.inputName);
      } else if (!this.value.match(validCharacters)) {
        this.error =
          this.inputName + " can only contain alphabetical characters.";
        this.$root.$refs.form.triggerError(this.inputName);
      } else {
        this.error = null;
        this.$root.$refs.form.removeError(this.inputName);
      }
    },
  },
};
</script>

<style lang="scss">
.inputField {
  border: solid 0.1rem #969696;
  border-radius: 0.2rem;
  height: 1.8rem;
}
.fancy-tag {
  font-weight: bold;
  font-size: medium;
  text-transform: capitalize;
}
.fieldGroup {
  align-items: start;
}
.error {
  color: red;
  margin: 0;
  font-size: 0.9rem;
  text-align: left;
  &::first-letter {
    text-transform: capitalize;
  }
  max-width: 12rem;
}
</style>
